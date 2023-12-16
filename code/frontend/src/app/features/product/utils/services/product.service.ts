import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { __param } from 'tslib';
import { ApiBaseContentModel } from 'src/app/shared';
import { addTransaction, getProductByIdResponce, getProductsRequest, getProductsResponce } from '../models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ProductService {
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  serviceUrl: string = environment.serverUrl + "/product"

  constructor(private http: HttpClient) {
    this.serviceUrl =environment.serverUrl + "/product"
    
  }

  getProduct(id: number) {
    return this.http.get<ApiBaseContentModel<getProductByIdResponce>>(this.serviceUrl + '/'+ id, this.httpOptions);
  }

  getAll(request: getProductsRequest) {

    let params = this.getParam(request);
    return this.http.get<ApiBaseContentModel<getProductsResponce[]>>(this.serviceUrl + '/list', { params, ...this.httpOptions });
  }

  createProduct(body: any) {
    return this.http.post<ApiBaseContentModel<boolean>>(this.serviceUrl + '/create', body,  this.httpOptions);

  }

  updateProduct(body: any) {
    return this.http.post<ApiBaseContentModel<boolean>>(this.serviceUrl + '/update', body,  this.httpOptions);

  }

  deleteProduct(id: any) {
    return this.http.post<ApiBaseContentModel<boolean>>(this.serviceUrl + '/delete'+ '/'+ id, null, this.httpOptions);

  }

  createtransaction(body: addTransaction) {
    return this.http.post<ApiBaseContentModel<boolean>>(this.serviceUrl + '/transaction', body,  this.httpOptions);

  }

  //#region private methods
  private getParam(param: any): HttpParams{
    let params = new HttpParams();
    // Iterate over the properties of the param object
    for (const key in param) {
      if (param.hasOwnProperty(key) && param[key] !== undefined && param[key] !== null && param[key] !== '') {
        // Add non-null and non-undefined and non empty properties to HttpParams
        params = params.set(key, param[key].toString());
      }
    }
    return params;
  }
  //#endregion
}
