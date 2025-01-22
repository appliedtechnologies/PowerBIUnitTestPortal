// src/app/components/report-list/report-list.component.ts
import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import ODataStore from 'devextreme/data/odata/store';
import { ClickEvent } from 'devextreme/ui/button';
import { PowerbiService } from 'src/app/shared/services/report.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './reportlist.component.html',
  styleUrls: ['./reportlist.component.css'],
})
export class ReportListComponent {
  public dataSourceReports: DataSource;
  dataSource! : ODataStore;  
  columns: any[] = [
    { dataField: 'Id', caption: 'ID', dataType: 'number' },
    { dataField: 'ReportId', caption: 'Report ID', dataType: 'string' },
    { dataField: 'WorkspaceId', caption: 'Workspace ID', dataType: 'string' },
    { dataField: 'Tenant', caption: 'Tenant', dataType: 'number' },
    { dataField: 'Name', caption: 'Name', dataType: 'string' },
  ];
  
  @ViewChild(DxDataGridComponent, { static: false })
  dataGrid: DxDataGridComponent;

  constructor(private reportService: PowerbiService) {
    this.dataSourceReports = new DataSource({
        store: reportService.getStore()
    });
  }

  public onClickRefresh(e: ClickEvent): void {
    this.dataGrid.instance.refresh();
}
  
}
