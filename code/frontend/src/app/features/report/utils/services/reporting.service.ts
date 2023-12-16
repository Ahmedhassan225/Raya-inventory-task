import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductReportRequest } from '../models/app-models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportingService {
  serviceUrl: string = environment.serverUrl +  "/Report"

  constructor(private http: HttpClient) {
    this.serviceUrl = environment.serverUrl + "/Report"
    
  }
  getReport(searchModel: ProductReportRequest){
      let params = this.getParam(searchModel);
      return this.http.get<any>(this.serviceUrl , { params, responseType: 'arraybuffer' as 'json' });
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
