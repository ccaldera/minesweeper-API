export class IGame {
    id:string;
    userId:string;
    board:ICell[][];
    status:string;
    creationDate: Date;
    endDate: Date;
}

export class ICell{
    status:string;
    value?:number;
}