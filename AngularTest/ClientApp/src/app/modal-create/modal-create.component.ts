import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { validate } from 'json-schema';
import { Contacto } from '../models/contacto.interface';

@Component({
  selector: 'app-modal-create',
  templateUrl: './modal-create.component.html',
  styleUrls: ['./modal-create.component.css']
})
export class ModalCreateComponent implements OnInit {

  contactoForm: FormGroup;
  contacto: Contacto;

  constructor(public modal: NgbActiveModal) { }

  ngOnInit() {

    this.contactoForm = new FormGroup({
      'id': new FormControl(this.contacto.id),
      'nombre': new FormControl(this.contacto.nombre, [Validators.required]),
      'direccion': new FormControl(this.contacto.direccion),
      'telefono': new FormControl(this.contacto.telefono, [Validators.required]),
      'curp': new FormControl(this.contacto.curp),
      'fechaRegistro': new FormControl(this.contacto.fechaRegistro)
    })

  }

  get nombre() {
    return this.contactoForm.get('nombre');
  }

  get telefono() {
    return this.contactoForm.get('telefono');
  }

  onSubmit() {
    this.modal.close(this.contactoForm.value);
  }

}
