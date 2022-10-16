import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contactos',
  templateUrl: './contactos.component.html'
})
export class ContatosComponent {
  public contactos: Contacto[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Contacto[]>(baseUrl + 'api/contactos').subscribe(result => {
      this.contactos = result;
    }, error => console.error(error));
  }
}

interface Contacto {
  id: number;
  nombre: string;
  direccion: string;
  telefono: string;
  curp: string;
  fechaRegistro: Date
}
