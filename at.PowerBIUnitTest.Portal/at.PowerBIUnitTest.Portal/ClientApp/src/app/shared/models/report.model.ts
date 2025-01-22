
import { Tenant } from "./tenant.model";

export interface Report {
    Id?: number;
    ReportId?: string;
    WorkspaceId?: string;   
    Tenant?: number | null;
    TenantNavigation?: Tenant;
    Name?: string;

}