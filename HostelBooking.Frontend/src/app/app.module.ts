import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from "@auth0/angular-jwt";
import { CATALOGUE_API_URL, SECURITY_API_URL } from "./app-injection-tokens";
import { environment } from "../environments/environment";
import { NavComponent } from './components/nav/nav.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { AppMaterialModule } from "./app-material.module";
import { ReactiveFormsModule } from '@angular/forms';
import {CatalogueComponent} from "./components/catalogue/catalogue.component";

export function getJWT(){
  return localStorage.getItem('jwt_response');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    SignInComponent,
    SignUpComponent,
    CatalogueComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    ReactiveFormsModule,

    JwtModule.forRoot({
      config: {
        tokenGetter: getJWT
      }
    })
  ],
  providers: [
    {
      provide: SECURITY_API_URL,
      useValue: environment.securityApi
    },
    {
      provide: CATALOGUE_API_URL,
      useValue: environment.catalogueApi
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
