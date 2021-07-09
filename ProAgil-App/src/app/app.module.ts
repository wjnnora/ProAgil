import { EventoService } from './_service/evento.service';
import { DatetimeformatpipePipe } from './_utils/datetimeformatpipe.pipe';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [		
    AppComponent,
      EventosComponent,
      NavComponent,
      DatetimeformatpipePipe
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    EventoService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
