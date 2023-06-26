
export class Customer {

  public constructor(init?: Partial<Customer>) {
    Object.assign(this, init);
  }

  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  email: string;
  bankAccountNumber: string
}

