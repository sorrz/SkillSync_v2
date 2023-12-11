// StudentModel.ts

interface StudentModel {
  id: number;
  name: string;
  mailAddress: string;
  passwordHash: string;
  techStack: string[];
  phoneNumber: string;
  graduation: string; // You may want to use a Date type here if graduation is a date
  startLia1: string; // Use Date type if lia1Start is a date
  endLia1: string; // Use Date type if lia1End is a date
  startLia2: string; // Use Date type if lia2Start is a date
  endLia2: string; // Use Date type if lia2End is a date
  presentation: string;
  imageUrl: string;
  connectedTo: string[];
  linkedInProfile: string;
}

export default StudentModel;
