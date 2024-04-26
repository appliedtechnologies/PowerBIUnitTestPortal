import { Component, OnInit, ViewChild } from "@angular/core";
import { LayoutParameter, LayoutService, NotificationType, } from "src/app/shared/services/layout.service";
import notify from "devextreme/ui/notify";
import { WorkspaceService } from "src/app/shared/services/workspace.service";
import { Workspace } from "src/app/shared/models/workspace.model";
import { UnitTestService } from "src/app/shared/services/UnitTest.service";
import { UnitTest } from "src/app/shared/models/UnitTest.model";
import { Structur } from "src/app/shared/models/Structur.model";
import { UserStoryService } from "src/app/shared/services/UserStory.service";
import { UserStory } from "src/app/shared/models/UserStory.model";
import { TabularModel } from "src/app/shared/models/TabularModel.model";
import { DxTreeListComponent } from "devextreme-angular";
import { TestRuns } from "src/app/shared/models/TestRuns.model";
import { TestRunStructur } from "src/app/shared/models/TestRunStructur.model";
import { TabularModelStructure } from "src/app/shared/models/TabularModelStructure";
import DataSource from "devextreme/data/data_source";


@Component({
  selector: "app-unit-tests",
  templateUrl: "./unit-tests.component.html",
  styleUrls: ["./unit-tests.component.css"],
})
export class UnitTestsComponent {
  public dataSourceWorkspaces: DataSource;

  public unitTestToEdit: UnitTest;
  public unitTestToDelete: UnitTest;
  public userStoryToEdit: UserStory;
  public userStoryToCopy: UserStory;

  public isPopupVisibleUTEdit: boolean = false;
  public isPopupVisibleUT: boolean = false;
  public isPopupVisibleUS: boolean = false;
  public isPopupVisibleTR: boolean = false;
  public isPopupVisibleTB: boolean = false;
  public isPopupVisibleUSedit: boolean = false;
  public isPopupVisibleDelete: boolean = false;

  public ResultTypes: string[] = ["String", "Float", "Date", "Percentage"];
  public DateTimeFormats: string[] = ["UTC", "CET"];
  public DateTimeFormats2: string[] = ["Long", "Short"];
  public Seperators: string[] = ["Use Seperators", "Dont use Sperators"];
  public decimal: string[] = ["1", "2", "3", "4", "5"];
  public IsTypeString: boolean = false;
  public IsTypeDate: boolean = false;
  public IsTypeFloat: boolean = false;

  @ViewChild(DxTreeListComponent, { static: false }) treeList: DxTreeListComponent;

  constructor(
    private workspaceService: WorkspaceService,
    private UnitTestService: UnitTestService,
    private UserStoryService: UserStoryService,
    private LayoutService: LayoutService) {
    this.dataSourceWorkspaces = new DataSource({
      store: this.workspaceService.getStore(),
      expand: ["TabularModels", "TabularModels.UserStories", "TabularModels.UserStories.UnitTests"]
    });

    this.showUnitTestEdit = this.showUnitTestEdit.bind(this);
    this.showUserStoryEdit = this.showUserStoryEdit.bind(this);
    this.checkType = this.checkType.bind(this);
    this.deleteUnitTest = this.deleteUnitTest.bind(this);
    this.showUnitTest = this.showUnitTest.bind(this);
    this.showUserStory = this.showUserStory.bind(this);
    this.executeUnitTestsForRow = this.executeUnitTestsForRow.bind(this);
    this.editUserStory = this.editUserStory.bind(this);
    this.togglePopupDelete = this.togglePopupDelete.bind(this);
    this.showDelete = this.showDelete.bind(this);
    this.deleteWorkspace = this.deleteWorkspace.bind(this);
    this.showTestRuns = this.showTestRuns.bind(this);
    this.showTabModel = this.showTabModel.bind(this);
    this.showTestRunsLocal = this.showTestRunsLocal.bind(this);
    this.onClickCopyUserStory = this.onClickCopyUserStory.bind(this);
  }

  public onClickCopyUserStory(e: any): void {
    const userStoryId = this.userStoryToCopy.Id;
    const targetTabularModelId = e.row.data.Id;
    const targetWorkspaceId = e.row.data.Workspace;

    this.UserStoryService.copyToOtherTabularModel(userStoryId, targetTabularModelId, targetWorkspaceId)
      .then(() => {
        notify("UserStory wurde kopiert", "success", 2000);
        // Fügen Sie hier die gewünschten Aktualisierungen oder Benachrichtigungen hinzu
      })
      .catch((error) => {
        console.error("Fehler beim Kopieren der UserStory", error);
        // Hier können Sie Fehlerbehandlung oder Benachrichtigungen hinzufügen
      });
    this.isPopupVisibleTB = false;
  }

  public showTabModel(e: any) {
    this.userStoryToCopy = new UserStory();
    this.userStoryToCopy.Id = e.row.data.Id;
    this.isPopupVisibleTB = true;
  }

  public showTestRuns() {
    this.isPopupVisibleTR = true;
  }

  public showTestRunsLocal(e: any): void {
    //this.header = "Testläufe von " + e.row.data.Typ + ": " + e.row.data.Name;
    /*this.StructurService.GetTestRunStructureForWorkspace(e.row.data.Name).then((TRstructure) => {
      this.TestRunsStructures = TRstructure;
    });*/
    this.isPopupVisibleTR = true;
  }


  public showUnitTestEdit(e: any): void {
    let unitTestStructure: Structur;
    if (e.row == undefined) {
      if (e.data.Typ != "UnitTest") // disable double click for non-UnitTest rows
        return;
      unitTestStructure = e.data;
    }
    else
      unitTestStructure = e.row.data;

    let unitTest: UnitTest = {
      Id: unitTestStructure.Id,
      Name: unitTestStructure.Name,
      ExpectedResult: unitTestStructure.ExpectedResult,
      LastResult: unitTestStructure.LastResult,
      Timestamp: unitTestStructure.Timestamp,
      UserStory: unitTestStructure.UserStory,
      DAX: unitTestStructure.DAX
    };
    console.log("this.toggle", this);
    this.togglePopupUTEdit(unitTest);

  }

  public showUserStoryEdit(e: any): void {
    let userStory: UserStory = {
      Id: e.row.data.Id,
      Beschreibung: e.row.data.Name,
      TabularModel: e.row.node.parent.data.Id
    };

    this.togglePopupUSEdit(userStory);
  }

  public showUnitTest(e: any): void {
    let userStoryId: number = e.row.data.Id;
    this.togglePopupUT(userStoryId);
  }

  public showUserStory(e: any): void {
    let tabularModelId: number = e.row.data.Id;
    this.togglePopupUS(tabularModelId);
  }

  public showDelete(e: any) {
    let unitTestStructure: Structur;
    if (e.row == undefined) {
      if (e.data.Typ != "UnitTest") // disable double click for non-UnitTest rows
        return;
      unitTestStructure = e.data;
    }
    else
      unitTestStructure = e.row.data;


    this.unitTestToDelete = {
      Id: unitTestStructure.Id,
      Name: unitTestStructure.Name,
      ExpectedResult: unitTestStructure.ExpectedResult,
      LastResult: unitTestStructure.LastResult,
      Timestamp: unitTestStructure.Timestamp,
      UserStory: unitTestStructure.UserStory,
      DAX: unitTestStructure.DAX
    };
    let unitTestX: UnitTest;
    unitTestX = this.unitTestToDelete;
    console.log("this.toggle", this);
    this.togglePopupDelete(unitTestX);
  }

  public onCellPrepared(e) {
    if (e.rowType === "data" && e.column.dataField === "LastResult") {
      e.cellElement.style.color = e.data.ExpectedResult == e.data.LastResult ? "green" : "red";
    }
  }
  public onRowPrepared(e: any): void {
    if (e.rowType == "data" && e.data.Typ != "UnitTest" || e.rowType == "header") {
      e.rowElement.classList.add("hide-checkbox");
    }
  }

  public addUserStory(e) {
    this.UserStoryService.getStore().insert(this.userStoryToEdit).then(() => {
      notify("UserStory '" + this.userStoryToEdit.Beschreibung + "' wurde hinzugefügt", "success", 2000);
      this.togglePopupUS();
    })
  }

  public addUnitTest(e) {
    this.UnitTestService.getStore().insert(this.unitTestToEdit).then(() => {
      notify("UnitTest '" + this.unitTestToEdit.Name + "' wurde hinzugefügt", "success", 2000);
      this.togglePopupUT();
    })
  }

  public editUnitTest(e) {
    this.UnitTestService.getStore().update(this.unitTestToEdit.Id, this.unitTestToEdit).then(() => {
      notify("UnitTest '" + this.unitTestToEdit.Name + "' wurde bearbeitet", "success", 2000);
      this.togglePopupUTEdit();
    })
  }

  public editUserStory(e) {
    this.UserStoryService.getStore().update(this.userStoryToEdit.Id, this.userStoryToEdit).then(() => {
      notify("UserStory '" + this.userStoryToEdit.Beschreibung + "' wurde bearbeitet", "success", 2000);
      this.togglePopupUSEdit();
    })
  }

  confirmDelete(answer: string, e: any): void {
    this.isPopupVisibleDelete = false; // Close the popup
    if (answer === 'yes') {
      this.deleteUnitTest(e);
    }
  }

  public deleteUnitTest(e: any) {
    let id: number = e.Id;
    let name: string = e.Name;

    //this.isPopupVisibleDelete = true


    this.UnitTestService.getStore().remove(id).then(() => {
      notify("UnitTest '" + name + "' wurde gelöscht", "success", 2000);
    });
  }

  public deleteWorkspace(e: any) {
    let id: number = e.row.data.Id;
    let name: string = e.row.data.Name;

    this.workspaceService.getStore().remove(id).then(() => {
      notify("Workspace '" + name + "' wurde gelöscht", "success", 2000);
    });
  }

  public deleteUnitTestsForSelection(e: any): void {
    this.LayoutService.change(LayoutParameter.ShowLoading, true);
    let results: void[] = new Array();
    let selectedRowsData: any[] = this.treeList.instance.getSelectedRowsData();

    if (selectedRowsData.length == 0)
      notify("No unit tests were selected", "error", 2000)
    else
      selectedRowsData.forEach(r => {
        this.UnitTestService.getStore().remove(r.Id).then(() => {
          notify("UnitTest '" + r.Name + "' wurde gelöscht", "success", 2000);
        });
      });
  }

  public LoadWorkspace(event) {
    this.UnitTestService.LoadWorkspace();
    notify("Workspaces werden geladen", "success", 6000);
  }

  public executeUnitTestsForRow(e: any): void {
    this.LayoutService.change(LayoutParameter.ShowLoading, true);
    let rowData: Structur = e.row.data;
    delete (rowData as any).id;
    this.executeUnitTests(rowData).then((values) => {
      notify("All selected unit tests have been executed!", "success", 2000);
      this.LayoutService.change(LayoutParameter.ShowLoading, false);
    });
  }

  public isWorkspaceRow(e: any): boolean {
    if (e.row.rowType == "data" && e.row.data.Typ == "Workspace")
      return true;
    return false;
  }

  public isUnitTestRow(e: any): boolean {
    if (e.row.rowType == "data" && e.row.data.Typ == "UnitTest")
      return true;
    return false;
  }

  public isUserStoryRow(e: any): boolean {
    if (e.row.rowType == "data" && e.row.data.Typ == "UserStory")
      return true;
    return false;
  }

  public isTabularModelRow(e: any): boolean {
    if (e.row.rowType == "data" && e.row.data.Typ == "TabularModel")
      return true;
    return false;
  }

  public executeUnitTestsForSelection(e: any): void {
    this.LayoutService.change(LayoutParameter.ShowLoading, true);
    let results: Promise<void[]>[] = new Array();
    let selectedRowsData: Structur[] = this.treeList.instance.getSelectedRowsData();

    if (selectedRowsData.length == 0)
      notify("No unit tests were selected", "error", 2000)
    else
      selectedRowsData.forEach(r => {
        delete (r as any).id;
        results.push(this.executeUnitTests(r));
      });

    Promise.all(results).then((values) => {
      notify("All selected unit tests have been executed!", "success", 2000);
      this.LayoutService.change(LayoutParameter.ShowLoading, false);
    });
  }

  private togglePopupUT(userStoryId: number = undefined): void {
    this.isPopupVisibleUT = !this.isPopupVisibleUT;
    if (this.isPopupVisibleUT == true && userStoryId != undefined) {
      this.unitTestToEdit = new UnitTest();
      this.unitTestToEdit.UserStory = userStoryId;
    }
  }

  private togglePopupUS(tabularModelId: number = undefined): void {
    this.isPopupVisibleUS = !this.isPopupVisibleUS;
    if (this.isPopupVisibleUS == true && tabularModelId != undefined) {
      this.userStoryToEdit = new UserStory();
      this.userStoryToEdit.TabularModel = tabularModelId;
    }
  }

  private togglePopupUTEdit(unitTest: UnitTest = undefined): void {
    this.isPopupVisibleUTEdit = !this.isPopupVisibleUTEdit;
    if (this.isPopupVisibleUTEdit == true && unitTest != undefined) {
      this.unitTestToEdit = unitTest;
    }
  }

  private togglePopupUSEdit(userStory: UserStory = undefined): void {
    this.isPopupVisibleUSedit = !this.isPopupVisibleUSedit;
    if (this.isPopupVisibleUSedit == true && userStory != undefined) {
      this.userStoryToEdit = userStory;
    }
  }

  public togglePopupDelete(unitTest: UnitTest = undefined): void {
    this.isPopupVisibleDelete = !this.isPopupVisibleDelete;
    if (this.isPopupVisibleDelete == true && unitTest != undefined) {
      this.unitTestToDelete = unitTest;
    }
  }


  public checkType(type: any) {
    if (type.value == 'String') {
      this.IsTypeString = true;
      this.IsTypeFloat = false;
      this.IsTypeDate = false;
    }

    if (type.value == 'Float') {
      this.IsTypeFloat = true;
      this.IsTypeString = false;
      this.IsTypeDate = false;
    }

    if (type.value == 'Date') {
      this.IsTypeDate = true;
      this.IsTypeFloat = false;
      this.IsTypeString = false;
    }
  }

  private executeUnitTests(structur: Structur): Promise<void[]> {
    let results: Promise<void>[] = new Array();

    /*if (structur.Typ == "Workspace")
      results = this.executeUnitTestsOfWorkspace(this.structures.find(s => s.Id == structur.Id));
    else if (structur.Typ == "TabularModel")
      results = this.executeUnitTestsOfTabularModel(this.structures.find(s => s.WorkspacePbId == structur.WorkspacePbId).items.find(s => s.Typ == "TabularModel" && s.Id == structur.Id));
    else if (structur.Typ == "UserStory")
      results.push(this.executeUnitTestsOfUserStory(this.structures.find(s => s.WorkspacePbId == structur.WorkspacePbId).items.find(s => s.items.find(u => u.Id == structur.Id) != undefined).items.find(s => s.Typ == "UserStory" && s.Id == structur.Id)));
    else if (structur.Typ == "UnitTest")
      results.push(this.UnitTestService.executeUnitTest(structur));*/

    return Promise.all(results);
  }

  private executeUnitTestsOfUserStory(userstory: any): Promise<void> {
    return new Promise<void>((resolve) => {
      let results: Promise<void>[] = new Array();
      let Result: string;
      let Count: number;
      let i: number
      i = 0;
      Result = "True";

      userstory.items.forEach((unitTest: UnitTest) => {
        results.push(this.UnitTestService.executeUnitTest(unitTest as Structur).then(() => {
          if (this.UnitTestService.wasUnitTestSuccessful == false)
            Result = "False"
        }));
        i++;
        Count = i;

      })

      Promise.all(results).then(() => {
        this.UnitTestService.SaveTestRun(Result, "UserStory", Count, userstory.Name);

        resolve();
      });
    })
  }

  private executeUnitTestsOfTabularModel(tabularModel: any): Promise<void>[] {
    let results: Promise<void>[] = new Array();
    let Result: string;
    let Count: number;
    let i: number
    i = 0;
    Result = "True";

    tabularModel.items.forEach((userStory: any) => {
      userStory.items.forEach((unitTest: UnitTest) => {
        results = results.concat(this.UnitTestService.executeUnitTest(unitTest as Structur).then(() => {
          if (this.UnitTestService.wasUnitTestSuccessful == false)
            Result = "False"
        }

        ));
        i++
        Count = i

      })

    })

    Promise.all(results).then(() => {
      this.UnitTestService.SaveTestRun(Result, "TabularModel", Count, tabularModel.Name);
    });

    return results;
  }

  private executeUnitTestsOfWorkspace(workspace: any): Promise<void>[] {
    let results: Promise<void>[] = new Array();
    let Result: string;
    let Count: number;
    let i: number
    let Histories: History[];
    i = 0;
    Result = "True";

    workspace.items.forEach((tabularModel: any) => {
      tabularModel.items.forEach((userStory: any) => {
        userStory.items.forEach((unitTest: UnitTest) => {
          results = results.concat(this.UnitTestService.executeUnitTest(unitTest as Structur).then(() => {
            if (this.UnitTestService.wasUnitTestSuccessful == false)
              Result = "False"

          }

          ));
          i++
          Count = i


        })

      })
    })

    Promise.all(results).then(() => {
      this.UnitTestService.SaveTestRun(Result, "Workspace", Count, workspace.Name);
    });

    return results;
  }
} 