import StudentModel from "../Models/StudentModel";
import { CompanyModel } from "../Models/CompanyModel";
import axios from 'axios';

const apiUrl =`${import.meta.env.VITE_API_URL}`;

export const getAllCompanies =async () => {
    return await axios.get(apiUrl) //Add Location for Company Endpoint
    .then(response => response.data)
}

export const getCompanyById =async (id? : number) => {
    return await axios.get(apiUrl+`/${id}`) //Add Location for Company Endpoint
    .then(response => response.data)
}

export const getAllStudents = async () => {
    return await axios.get(apiUrl)  // ADD Location for Student Endpoint
    .then(response => response.data)
}

export const getStudentById = async (id? : number) => {
    return await axios.get(apiUrl+`/${id}`)  // ADD Location for Student Endpoint
    .then(response => response.data)
}

export const updateCompanyById = async ( company : CompanyModel ) => {
    return await axios.put(apiUrl, company)
    .then(response => response.data)
}

export const updateStudentById = async ( student : StudentModel ) => {
    return await axios.put(apiUrl, student)
    .then(response => response.data)
}


// SHOULD WE INCLUDE THEESE IN THE SERVICE CLASS? 
export const deleteCompanyById = async ( id?: number ) => {
    if(id === null) return;

    return await axios.delete(apiUrl+`/${id}`)
    .then(response => response.data)
}

export const deleteStudentById = async ( id?: number ) => {
    if(id === null) return;

    return await axios.delete(apiUrl+`/${id}`)
    .then(response => response.data)
}