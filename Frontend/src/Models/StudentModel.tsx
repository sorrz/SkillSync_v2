// StudentModel.tsx

interface StudentModel {
  Id: number;
  Name: string;
  MailAddress: string;
  PasswordHash: string;
  TechStack: string[];
  PhoneNumber: string;
  Graduation: string;
  StartLia1: string;
  EndLia1: string;
  StartLia2: string;
  EndLia2: string;
  Presentation: string;
  ImageUrl: string;
  connectedTo: string[];
}

export default StudentModel;
