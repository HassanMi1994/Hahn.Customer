import { Component, HostBinding } from '@angular/core';
import { ChildrenOutletContexts } from '@angular/router';
import { slideInAnimation } from './animation';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [slideInAnimation]
},
)
export class AppComponent {
  title = 'Hahn.FrontEnd';
  constructor() {

  }

  prepareRouteTransition(outlet:any) {
    const animation = outlet.activatedRouteData['animation'] || {};
   console.warn(animation['value']);
    return animation['value'] || null;
  }
}
