import { Injectable } from '@angular/core';
import { Test } from 'tslint';
import { Structur } from '../models/Structur.model';
import { UnitTest } from '../models/UnitTest.model';
import { Workspace } from '../models/workspace.model';
import ODataContext from "devextreme/data/odata/context";
import { AppConfig } from '../config/app.config';
import { HttpClient } from "@angular/common/http";
import { ODataService } from './odata.service';
import { TestRuns } from '../models/TestRuns.model';
import { TestRunStructur } from '../models/TestRunStructur.model';
import { History } from '../models/History.model';
import { UnitTestService } from './UnitTest.service';
import HierarchicalCollectionWidget from 'devextreme/ui/hierarchical_collection/ui.hierarchical_collection_widget';
var structures: Structur[];

@Injectable()
export class StructurService {

    constructor(
        private odataService: ODataService,
        //private UserStoryService: UserStoryService,
        private http: HttpClient,
        private UnitTestService: UnitTestService,


    ) { }


    LoadWorkspace() {
        let request = this.http
            .post(`${AppConfig.settings.api.url}/UnitTests/LW`, null)
            .subscribe(() => { console.log("OK") });

    }

    getWorkspaces2(): Promise<Workspace[]> {
        return new Promise<Workspace[]>((resolve, reject) => {
            let request = this.http
                .post<Workspace[]>(`${AppConfig.settings.api.url}/Workspaces/FilterWorkspace`, null)
                .subscribe(
                    response => {
                        resolve(response["value"]);
                    }
                );

        });

    }

    getRuns(): Promise<TestRuns[]> {
        return new Promise<TestRuns[]>((resolve, reject) => {
            let request = this.http
                .get<TestRuns[]>(`${AppConfig.settings.api.url}/TestRuns?$expand=HistoriesRun($expand=UnitTestNavigation)`)
                .subscribe(
                    response => {
                        resolve(response["value"]);
                    }
                );

        });

    }


   

    getStructure(Workspaces: Workspace[]): Promise<Structur[]> {
        return new Promise<Structur[]>((resolve, reject) => {
            var structures: Structur[] = [];

            this.getWorkspaces2().then((filter) => {
                Workspaces.forEach(Workspace => {
                    if (filter.some(p => p.WorkspacePbId === Workspace.WorkspacePbId.valueOf())) {

                        var structureWorkspace: Structur = {
                            Typ: "Workspace",
                            Name: Workspace.Name,
                            WorkspacePbId: Workspace.WorkspacePbId,
                            DatasetPbId: null,
                            ExpectedResult: "",
                            DAX: "",
                            Timestamp: null,
                            LastResult: "",
                            Id: Workspace.Id,
                            UserStory: null,
                            UserStoryNavigation: null,
                            items: []
                        };

                        Workspace.TabularModels.forEach(TabularModel => {
                            var structureTabularModel: Structur = {
                                Typ: "TabularModel",
                                Name: TabularModel.Name,
                                WorkspacePbId: Workspace.WorkspacePbId,
                                DatasetPbId: TabularModel.DatasetPbId,
                                ExpectedResult: "",
                                DAX: "",
                                Timestamp: null,
                                LastResult: "",
                                Id: TabularModel.Id,
                                UserStory: null,
                                UserStoryNavigation: null,
                                items: []
                            };

                            TabularModel.UserStories.forEach(UserStory => {
                                var structureUserStory: Structur = {
                                    Typ: "UserStory",
                                    Name: UserStory.Beschreibung,
                                    WorkspacePbId: Workspace.WorkspacePbId,
                                    DatasetPbId: TabularModel.DatasetPbId,
                                    ExpectedResult: "",
                                    DAX: "",
                                    Timestamp: null,
                                    LastResult: "",
                                    Id: UserStory.Id,
                                    UserStory: null,
                                    UserStoryNavigation: null,
                                    items: []
                                };

                                UserStory.UnitTests.forEach(UnitTest => {
                                    var structureUnitTest: Structur = {
                                        Typ: "UnitTest",
                                        Name: UnitTest.Name,
                                        WorkspacePbId: Workspace.WorkspacePbId,
                                        DatasetPbId: TabularModel.DatasetPbId,
                                        ExpectedResult: UnitTest.ExpectedResult,
                                        DAX: UnitTest.DAX,
                                        Timestamp: UnitTest.Timestamp,
                                        LastResult: UnitTest.LastResult,
                                        Id: UnitTest.Id,
                                        UserStory: UnitTest.UserStory,
                                        UserStoryNavigation: UnitTest.UserStoryNavigation,
                                        items: []
                                    };
                                    structureUserStory.items.push(structureUnitTest)
                                })
                                structureTabularModel.items.push(structureUserStory)
                            })
                            structureWorkspace.items.push(structureTabularModel);
                        })
                        structures.push(structureWorkspace);
                    }
                });
                resolve(structures);
            });
        });
    }

    GetTestRunStructure(TestRuns: TestRuns[]): Promise<TestRunStructur[]>{
        return new Promise<TestRunStructur[]>((resolve, reject) => {
            var TRstructures: TestRunStructur[] = [];
            var TRname;
            var HistoryName
            var HistoryKey

            this.getRuns().then(() => {
                TestRuns.forEach(TestRun => {

                    if(TestRun.Workspace != null)
                    TRname = TestRun.Workspace;

                    if(TestRun.TabularModel != null)
                    TRname = TestRun.TabularModel;

                    if(TestRun.UserStory != null)
                    TRname = TestRun.UserStory;
                    
                    var structureTestRuns: TestRunStructur = {
                        Type: TestRun.Type,
                        TimeStamp: TestRun.TimeStamp,
                        Result: TestRun.Result,
                        Workspace: TestRun.Workspace,
                        TabularModel: TestRun.TabularModel,
                        UserStory: TestRun.UserStory,
                        Name: TRname,
                        items: [],

                        
                    };
 
                    TestRun.HistoriesRun.forEach(History => {
                        
                        //HistoryKey = this.UnitTestService.getStore().keyOf(History['UnitTest'])

                        /*this.UnitTestService.getStore().byKey(History.UnitTest).then((unitTest) => {
                            HistoryName = unitTest.Name;
                        })*/
                        //HistoryName = History.UnitTest.Name;

                        var StructureHitory: TestRunStructur ={
                            TimeStamp: History.TimeStamp,
                            Result: History.Result,
                            Name: History.UnitTestNavigation.Name,
                            Type: "/",
                            items: [],
                            Workspace: "",
                            TabularModel: "",
                            UserStory:"" 
                        }
                        structureTestRuns.items.push(StructureHitory)
                    })
                    TRstructures.push(structureTestRuns);

                });
                resolve(TRstructures);
            });
    
        
        });
   }

   GetTestRunStructureForWorkspace(workspaceName: string): Promise<TestRunStructur[]> {
    return new Promise<TestRunStructur[]>((resolve, reject) => {
        var TRstructures: TestRunStructur[] = [];
        var TRname;
        var HistoryName;
        var HistoryKey;

        this.getRuns().then((TestRuns) => {
            const filteredTestRuns = TestRuns.filter((TestRun) => {
                // Hier kannst du die Bedingung anpassen, um nach einem bestimmten Workspace zu filtern.
                return TestRun.Workspace === workspaceName || TestRun.TabularModel === workspaceName || TestRun.UserStory === workspaceName;
            });

            filteredTestRuns.forEach((TestRun) => {
                if (TestRun.Workspace != null) TRname = TestRun.Workspace;
                if (TestRun.TabularModel != null) TRname = TestRun.TabularModel;
                if (TestRun.UserStory != null) TRname = TestRun.UserStory;

                var structureTestRuns: TestRunStructur = {
                    Type: TestRun.Type,
                    TimeStamp: TestRun.TimeStamp,
                    Result: TestRun.Result,
                    Workspace: TestRun.Workspace,
                    TabularModel: TestRun.TabularModel,
                    UserStory: TestRun.UserStory,
                    Name: TRname,
                    items: [],
                };

                TestRun.HistoriesRun.forEach((History) => {
                    var StructureHistory: TestRunStructur = {
                        TimeStamp: History.TimeStamp,
                        Result: History.Result,
                        Name: History.UnitTestNavigation.Name,
                        Type: "/",
                        items: [],
                        Workspace: "",
                        TabularModel: "",
                        UserStory: "",
                    };
                    structureTestRuns.items.push(StructureHistory);
                });
                TRstructures.push(structureTestRuns);
            });
            resolve(TRstructures);
        });
    });
}

}