import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { ModalRemoveComponent } from '../modal-remove/modal-remove.component';

@Component({
  selector: 'app-contactos',
  templateUrl: './contactos.component.html'
})
export class ContatosComponent {
  public contactos: Contacto[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private modalService: NgbModal) {
    this.loadContactos();    
  }

  loadContactos() {

    this.http.get<Contacto[]>(this.baseUrl + 'api/contactos').subscribe(result => {
      this.contactos = result;
    }, error => console.error(error));

  }


  remove(contacto) {
    const modalRef = this.modalService.open(ModalRemoveComponent);
    modalRef.componentInstance.contacto = contacto;
    modalRef.result.then(((result) => {
      console.log(result);

      if (result === 'Ok') {

        this.http.delete(this.baseUrl + 'api/contactos/' + contacto.id).subscribe(result => {
          console.log(result);
          this.loadContactos();
        }, error => console.log(error));
      }
    }).bind(this))
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
