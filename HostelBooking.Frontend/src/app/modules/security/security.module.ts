import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {SecurityRoutingModule} from "./security-routing.module";
import {SignInComponent} from "./pages/sign-in/sign-in.component";
import {SignUpComponent} from "./pages/sign-up/sign-up.component";
import {MaterialModule} from "../../core/modules/material/material.module";

@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent
  ],
  imports: [
    BrowserModule,
    SecurityRoutingModule,
    MaterialModule
  ],
  exports: [
    SignInComponent
  ],
  providers: []
})

export class SecurityModule{}
