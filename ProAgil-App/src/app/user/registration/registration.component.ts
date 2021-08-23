import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: any;

  constructor(public fb: FormBuilder, private toastr: ToastrService) { }

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
    console.log("Cadastrando usuário");
  }

}
