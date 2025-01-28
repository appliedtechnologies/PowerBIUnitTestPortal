import { Component, ElementRef, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IEmbedConfiguration, IReportEmbedConfiguration } from 'embed';
import { ReportService } from 'src/app/shared/services/report.service';
import { DisplayOption, TokenType } from 'powerbi-models';

@Component({
  selector: 'app-powerbi-report',
  templateUrl: './powerbi-report.component.html',
  styleUrls: ['./powerbi-report.component.css']
})
export class PowerbiReportComponent implements OnInit {
  @ViewChild('reportContainer', { static: false }) reportContainer!: ElementRef;
  private reportId!: string;
  private workspaceId!: string;
  public embedConfig: IReportEmbedConfiguration;

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
        this.embedConfig = {
          type: 'report',
          id: this.reportId,
          embedUrl: 'https://app.powerbi.com/reportEmbed',
          accessToken: accessToken,
          tokenType: TokenType.Embed,
          settings: {
            filterPaneEnabled: false,
            navContentPaneEnabled: true,
            customLayout: {
              displayOption: DisplayOption.FitToPage
            }
          },
        };
      },
      (error) => {
        console.error('Error fetching Embed Token:', error);
      }
    );
  }
}
