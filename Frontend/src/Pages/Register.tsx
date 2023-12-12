// Register.tsx
import React from 'react';
import RegistrationForm from '../Components/RegistrationForm';
import StudentModel from '../Models/StudentModel';
import '../Styles/registration.css'

interface RegisterProps {
    handleRegistration: (student: StudentModel) => void;
  }

  const Register: React.FC<RegisterProps> = ({ handleRegistration }) => {
  return (
    <div className='container shadow p-3 mb-5 bg-white rounded mulish-font' data-testid="register-container">
      <h2 className="py-5 text-center">Register</h2>
      <RegistrationForm handleRegistration={handleRegistration} />
    </div>
  );
};

export default Register;
