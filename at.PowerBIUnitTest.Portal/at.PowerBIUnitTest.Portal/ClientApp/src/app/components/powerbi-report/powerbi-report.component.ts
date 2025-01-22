import { Component, ElementRef, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as pbi from 'powerbi-client';
import { ReportService } from 'src/app/shared/services/report.service';

@Component({
  selector: 'app-powerbi-report',
  templateUrl: './powerbi-report.component.html',
  styleUrls: ['./powerbi-report.component.css']
})
export class PowerbiReportComponent implements OnInit {
  @ViewChild('reportContainer', { static: false }) reportContainer!: ElementRef;
  private reportId!: string;
  private workspaceId!: string;

  constructor(
    private powerbiService: ReportService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.reportId = params['reportId'];
      this.workspaceId = params['workspaceId'];

      if (this.reportId && this.workspaceId) {
        this.loadReport();
      } else {
        console.error('Report ID or Workspace ID is missing in the route parameters.');
      }
    });
  }

  private loadReport() {
    this.powerbiService.getEmbedToken(this.reportId, this.workspaceId).then(
      (accessToken) => {
        const embedConfig: pbi.IEmbedConfiguration = {
          type: 'report',
          id: this.reportId,
          embedUrl: 'https://app.powerbi.com/reportEmbed',
          accessToken: accessToken,
          tokenType: pbi.models.TokenType.Embed,
          settings: {
            filterPaneEnabled: false,
            navContentPaneEnabled: true,
          },
        };

        const powerbi = new pbi.service.Service(
          pbi.factories.hpmFactory,
          pbi.factories.wpmpFactory,
          pbi.factories.routerFactory
        );

        powerbi.embed(this.reportContainer.nativeElement, embedConfig);
      },
      (error) => {
        console.error('Error fetching Embed Token:', error);
      }
    );
  }
}
