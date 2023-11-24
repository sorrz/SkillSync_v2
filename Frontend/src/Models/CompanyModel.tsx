export interface CompanyModel {
    id?: number,
      name: string,
      imgURL: string,
      contactName: string,
      contactMail: string,
      contactPhone: string,
      TechStack: string[],
      presentation: string,
      mentorship: boolean,
      spots: number,
      hasExJob: boolean,
}