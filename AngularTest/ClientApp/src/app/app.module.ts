import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ContatosComponent } from './contactos/contactos.component';

import { NgbModule, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ModalRemoveComponent } from './modal-remove/modal-remove.component';
import { ModalCreateComponent } from './modal-create/modal-create.component';
import { ContactoFilterPipe } from './pipe/contacto-filter.pipe';
import { ImportarComponent } from './importar/importar.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ContatosComponent,
    ModalRemoveComponent,
    ModalCreateComponent,
    ContactoFilterPipe,
    ImportarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'contactos', component: ContatosComponent },
      { path: 'importar', component: ImportarComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [
    ModalCreateComponent,
    ModalRemoveComponent
  ]
})
export class AppModule { }
