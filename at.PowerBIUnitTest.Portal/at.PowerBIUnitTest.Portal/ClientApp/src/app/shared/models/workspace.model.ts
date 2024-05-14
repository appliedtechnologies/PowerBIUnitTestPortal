import { TabularModel } from "./tabular-model.model";
import { Tenant } from "./tenant.model";

export interface Workspace {
    Id?: number;
    Name?: string;
    MsId?: string;
    UniqueIdentifier?: string;
    Tenant?: number | null;
    TenantNavigation?: Tenant;
    TabularModels?: TabularModel[];
    IsVisible?: boolean;
}