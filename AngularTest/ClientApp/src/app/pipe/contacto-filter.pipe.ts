import { PipeTransform, Pipe } from "@angular/core";
import { filter } from "minimatch";

@Pipe({
  name: 'contactoFilter'
})
export class ContactoFilterPipe implements PipeTransform {

  transform(data: any[], filterType: Object): any {
    //const results = [];
    if (!data || !filter) {
      return data;
    }

    return data.filter(rest => {
      //const { id, ...rest } = obj;
      return Object.keys(rest).reduce((acc, curr) => {
        return acc || rest[curr].toString().toLowerCase().includes(filterType.toString().toLowerCase());
      }, false);
    });
  }
}
