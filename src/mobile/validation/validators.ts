const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{6,20}$/;

interface IValidationError {
  message: string;
  setMessage: React.Dispatch<React.SetStateAction<string>>;
}
  
type ValidationFunc<T> = (value: T) => [boolean, T];

const validateLength = (value: string, minLength: number, maxLength: number, errorMessage: string): [boolean, string] => {
  if(value.length >= minLength && value.length <= maxLength) {
      return [true, ""];
  }

  return [false, errorMessage];
}

const validateRegex = (value: string, regex: RegExp, errorMessage: string): [boolean, string] => {
  if(regex.test(value)) {
      return [true, ""];
  }

  return [false, errorMessage];
}

const validateMinLength = <Type extends { length: number }>(data: Type, minVal: number, errorMessage: string): [boolean, string] => {
  if(data.length >= minVal) {
      return [true, ""];
  }

  return [false, errorMessage];
}

const validateMaxLength = <Type extends { length: number }>(data: Type, maxValue: number, errorMessage: string): [boolean, string] => {
  if(data.length <= maxValue) {
      return [true, ""];
  }

  return [false, errorMessage];
}

export { 
  validateLength, 
  validateRegex, 
  validateMinLength, 
  validateMaxLength, 
  emailRegex, 
  passwordRegex,
  ValidationFunc,
  IValidationError
};
