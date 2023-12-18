import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import { Workspace } from "../models/workspace.model";
import { UserStoryService } from "./UserStory.service"; 
@Injectable()
export class WorkspaceService {

  constructor(
    private odataService: ODataService,
    private userStoryService: UserStoryService,
    private http: HttpClient,

  ) { }

  getStore(): ODataStore {
    return this.odataService.context["Workspaces"]
  }
    getStore2(){
    let request = this.http
        .post<Workspace[]>(`${AppConfig.settings.api.url}/Workspaces/FilterWorkspace`, null)
        .subscribe(
          response => {
            return response;
          },
        );
  }

  getStore3(): ODataStore {
    return this.odataService.context["Workspaces/FilterWorkspace"]
  }

  getWorkspaces(): Promise<Workspace[]> {
    return new Promise<Workspace[]>((resolve, reject)=>{
      this.getStore().load({expand: ["TabularModels.UserStories.UnitTests"]}).then((data)=>{
        resolve(data as Workspace[]);
      }).catch((error)=>{
        reject(error);
      })
    });
    
  }

  getWorkspaces2(): Promise<Workspace[]> {
    return new Promise<Workspace[]>((resolve, reject)=>{
      let request = this.http
        .post<Workspace[]>(`${AppConfig.settings.api.url}/Workspaces/FilterWorkspace`, null)
        .subscribe(
          response => {
            resolve(response["value"]);
          },
        );
    });
    
  }

  
  
}