
import { Lote } from "./lote";
import { Palestrante } from "./palestrante";
import { RedeSocial } from "./redesocial";

export class Evento{

    constructor () {}

    id: number;
    local: string;  
    dataEvento: Date;
    tema: string;  
    qtdPessoas: number;
    imagePath: string;  
    telefone: string;  
    email: string;  
    lotes: Lote[];
    redesSociais: RedeSocial[];
    palestranteEventos: Palestrante[];
}