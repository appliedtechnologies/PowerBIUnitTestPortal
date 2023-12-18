import { TabularModel } from "./TabularModel.model";

export interface Workspace{
    
    Name?: string;
    Id?: number;
    WorkspacePbId?: string;

    TabularModels?: TabularModel[];

}