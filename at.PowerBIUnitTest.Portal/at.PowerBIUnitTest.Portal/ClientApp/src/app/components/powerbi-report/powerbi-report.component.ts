import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import * as pbi from 'powerbi-client';
import { PowerbiService } from 'src/app/shared/services/report.service';

@Component({
  selector: 'app-powerbi-report',
  templateUrl: './powerbi-report.component.html',
  styleUrls: ['./powerbi-report.component.css']
})
export class PowerbiReportComponent implements AfterViewInit {
  @ViewChild('reportContainer', { static: false }) reportContainer!: ElementRef;

  constructor(private powerbiService: PowerbiService) {}

  ngAfterViewInit() {
    // Hole den Token vom Backend
    this.powerbiService.getEmbedToken()/*.then({
      next: (response) => {
        const embedConfig: pbi.IEmbedConfiguration = {
          type: 'report',
          id: '6c5228cb-b66a-4a44-80f9-e942579a494c',
          embedUrl: 'https://app.powerbi.com/reportEmbed?reportId=6c5228cb-b66a-4a44-80f9-e942579a494c&autoAuth=true&ctid=fbfd2005-2cb0-4cea-9c07-d5ad0307112d',
          accessToken: response.accessToken, // Dynamischer Token
          tokenType: pbi.models.TokenType.Embed,
          settings: {
            filterPaneEnabled: false,
            navContentPaneEnabled: true
          }
        };

        // Power BI Dienst instanziieren
        const powerbi = new pbi.service.Service(
          pbi.factories.hpmFactory,
          pbi.factories.wpmpFactory,
          pbi.factories.routerFactory
        );

        // Bericht einbetten
        powerbi.embed(this.reportContainer.nativeElement, embedConfig);
      },
      error: (err) => {
        console.error('Fehler beim Abrufen des Embed Tokens:', err);
      }
    });*/
  }
}


