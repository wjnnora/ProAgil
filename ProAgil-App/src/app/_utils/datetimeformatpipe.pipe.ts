import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../_constants/constants';

@Pipe({
  name: 'datetimeformatpipe'
})
export class DatetimeformatpipePipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Constants.DATE_TIME_FTM);
  }

}
