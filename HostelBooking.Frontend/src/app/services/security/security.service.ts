import {Inject, Injectable} from '@angular/core'
import {HttpClient} from "@angular/common/http";
import {SECURITY_API_URL} from "../../app-injection-tokens";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {JwtReponse, RefreshPayload, SignInPayload, SignUpPayload} from "../../interfaces/security.interfaces";
import {Observable, tap} from "rxjs";
import {JWT_Reponse} from "../../consts";

@Injectable({
  providedIn: 'root'
})

export class SecurityService{

  jwtResponse? : any

  constructor(
    private http: HttpClient,
    @Inject(SECURITY_API_URL) private securityUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  )
  {
  }

  signIn(payload: SignInPayload) : Observable<JwtReponse>{
     return this.http.post<JwtReponse>(`${this.securityUrl}login`, payload)
       .pipe(
         tap(jwtResponse => {
           localStorage.setItem(JWT_Reponse, jwtResponse.access_token)
         })
       )
  }

  signUp(payload: SignUpPayload) : Observable<JwtReponse>{
    return this.http.post<JwtReponse>(`${this.securityUrl}register`, payload)
      .pipe(
        tap(jwtResponse => {
          localStorage.setItem(JWT_Reponse, jwtResponse.access_token)
        })
      )
  }

  refresh(payload: RefreshPayload) : Observable<JwtReponse>{
    return this.http.post<JwtReponse>(`${this.securityUrl}refresh`, payload)
      .pipe(
        tap(jwtResponse => {
          localStorage.setItem(JWT_Reponse, jwtResponse.refresh_token)
        })
      )
  }

  isAuthenticated() : boolean{
    this.jwtResponse = localStorage.getItem(JWT_Reponse);
    return this.jwtResponse && !this.jwtHelper.isTokenExpired(this.jwtResponse);
  }

  logout(): void{
     localStorage.removeItem(JWT_Reponse);
  }
}
