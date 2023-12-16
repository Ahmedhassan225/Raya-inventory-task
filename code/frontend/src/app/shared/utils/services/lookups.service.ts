import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { __param } from 'tslib';
import { ApiBaseContentModel, KVModel } from '../models';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})

export class LookupsService {
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  serviceUrl: string = environment.serverUrl + "/lookup"

  constructor(private http: HttpClient) {
    this.serviceUrl = environment.serverUrl + "/lookup"
    
  }

  getCategory() {
    return this.http.get<ApiBaseContentModel<KVModel[]>>(this.serviceUrl + '/category', this.httpOptions);
  }


}
