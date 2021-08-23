import { AuthService } from './../../_service/auth.service';
import { User } from './../../_model/user';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: any;
  user: User;

  constructor(public fb: FormBuilder, private toastr: ToastrService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required]],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]]
      }, {validator: this.comparePassword})
    });
  }

  comparePassword(fg: any) {
    const confirmPassword = fg.get('confirmPassword');
    if (confirmPassword.errors == null || 'mismatch' in confirmPassword.errors) {
      if (fg.get('password').value !== confirmPassword.value) {
        confirmPassword.setErrors({ mismatch: true });
      }
      else {
        confirmPassword.setErrors(null);
      }
    }
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign({ password: this.registerForm.get('passwords.password').value }, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
          this.toastr.success('Cadastrado com sucesso.');
          this.router.navigate(['/user/login']);
        }, error => {
          const erro = error.error;
          erro.forEach((item: { code: any; }) => {
            switch (item.code) {
              case 'DuplicateUserName':
                this.toastr.error('Já existe um cadastro com esses dados!');
                break;
              default:
                this.toastr.error(`Ocorreu um erro! ${item}`);
                break;
            }
          });
        }
      );
    }
  }

}
