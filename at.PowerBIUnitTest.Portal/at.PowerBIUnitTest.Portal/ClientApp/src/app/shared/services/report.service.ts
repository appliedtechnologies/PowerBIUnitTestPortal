import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ODataService } from "./odata.service";
import { AppConfig } from "../config/app.config";
import ODataStore from 'devextreme/data/odata/store';
import { CrudBaseService } from './crud-base.service';
import { Report } from '../models/report.model';

@Injectable({
  providedIn: 'root'
})
export class PowerbiService extends CrudBaseService<Report> { 

  constructor(
    private odataService: ODataService,
    private http: HttpClient) {super()}

   getEmbedToken(reportId: string, workspaceId: string): Promise<string> {
    const url = `${AppConfig.settings.api.url}/GetEmbedToken(workspaceId=${encodeURIComponent(workspaceId)},reportId=${encodeURIComponent(reportId)})`;

    return new Promise<string>((resolve, reject) => {
      this.http.get(url).subscribe({
        next: (response) => resolve(response["value"].toString()),
        error: (error: any) => reject(error?.error?.error),
      });
    });
  }
  

    getStore(): ODataStore {
      return this.odataService.context["Reports"];
    }
    
    getReports(): Observable<any[]> {
      const url = `${AppConfig.settings.api.url}/Reports`;
      return this.http.get<any[]>(url);
    }

}
