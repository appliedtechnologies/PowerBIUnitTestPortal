import { Pipe, PipeTransform } from '@angular/core';
import { LogService } from '../services/log.service';

@Pipe({
    name: 'getFirstElement'
})
export class GetFirstElementPipe implements PipeTransform {
    constructor(private logService: LogService) {}

    transform(array: any[]): any {
        return array?.length > 0 ? array[0] : null;
    }
}