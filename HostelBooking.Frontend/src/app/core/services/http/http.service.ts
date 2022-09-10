import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ApiMethod, CatalogueEndpoints, SecurityEndpoints} from "../consts/consts";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  requestSecurityCall(securityEndpoint: SecurityEndpoints, method: ApiMethod, data?: any){
    let response;

    switch (method){
      case ApiMethod.POST:
        response = this.http.post(`${environment.securityApi}${securityEndpoint}`, data)
        break;
      case ApiMethod.DELETE:
        response = this.http.delete(`${environment.securityApi}${securityEndpoint}`)
        break;
    }

    return response;
  }

  requestCatalogueCall(catalogueEndpoint: CatalogueEndpoints, method: ApiMethod, data?: any){
    let response;

    switch (method){
      case ApiMethod.GET:
        response = this.http.get(`${environment.catalogueApi}${catalogueEndpoint}`, data)
        break;
      case ApiMethod.POST:
        response = this.http.post(`${environment.catalogueApi}${catalogueEndpoint}`, data)
        break;
      case ApiMethod.PUT:
        response = this.http.put(`${environment.catalogueApi}${catalogueEndpoint}`, data)
        break;
      case ApiMethod.DELETE:
        response = this.http.delete(`${environment.catalogueApi}${catalogueEndpoint}`, data)
        break;
    }

    return response;
  }
}
