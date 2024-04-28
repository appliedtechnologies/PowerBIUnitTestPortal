import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import { UnitTest } from "src/app/shared/models/unit-test.model";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import { UserStory } from "../models/user-story.model";
@Injectable()
export class UserStoryService {

  constructor(
    private odataService: ODataService,
    private http: HttpClient,
  ) { }

  public getStore(): ODataStore {
    return this.odataService.context["UserStories"];
  }

  public add (userStory: UserStory): Promise<void> {
    return this.getStore().insert(userStory);
  }

  public remove (id: number): Promise<void> {
    return this.getStore().remove(id);
  }

  public update (id: number, userStory: UserStory): Promise<void> {
    return this.getStore().update(id, userStory);
  }

  public copyToOtherTabularModel(userStoryId: number, targetTabularModelId: number, targetWorkspaceId: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      let request = this.http
        .post(`${AppConfig.settings.api.url}/UserStories(${userStoryId})/Copy2`, {
          targetTabularModelId1: targetTabularModelId,
             targetWorkspaceId1: targetWorkspaceId,
             userStoryId1: userStoryId,
          
        })
        .subscribe({
          next: (data) => resolve(),
          error: (error) => reject(error),
        });
    });
  }
}