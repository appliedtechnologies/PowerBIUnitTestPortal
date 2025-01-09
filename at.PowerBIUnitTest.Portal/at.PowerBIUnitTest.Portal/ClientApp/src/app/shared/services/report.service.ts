import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ODataService } from "./odata.service";
import { AppConfig } from "../config/app.config";

@Injectable({
  providedIn: 'root'
})
export class PowerbiService {
  private tokenUrl = 'https://your-backend.com/api/powerbi-token'; // Backend-Endpunkt

  constructor(private http: HttpClient) {}

  
   /**
   * Generate Embed Token
   * @param tenantId - Azure AD Tenant ID
   * @param accessToken - User's Azure AD Access Token
   * @returns Promise<string> - Embed token as string
   */
   public getEmbedToken(): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      this.http
        .post<{ token: string }>('https://api.powerbi.com/v1.0/myorg/groups/980c21bf-18c4-4210-8e66-027a5cea0e97/reports/e91f92b4-7566-4f75-b171-cd0590b15060/GenerateToken`,', {
        accessLevel: 'View'})
        .subscribe({
          next: (response) => resolve(response.token),
          error: (error: any) => reject(error?.error?.error),
        });
    });
  }
}
