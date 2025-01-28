import { Tenant } from "./tenant.model";

export interface Report {
    Id?: number;
    ReportId?: string;
    WorkspaceId?: string;
    Tenant?: number;
    TenantNavigation?: Tenant;
    Name?: string;
    RLSRole?: string;
}