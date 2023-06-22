import { Pipe, PipeTransform } from "@angular/core";

@Pipe({

    name: 'readableTitleCase'

})

export class ReadableTitleCase implements PipeTransform {


    transform(value: string): string {

        const result = value.replace(/([A-Z])/g, " $1");
        const finalResult = result.charAt(0).toUpperCase() + result.slice(1);
        return finalResult;
    }

}