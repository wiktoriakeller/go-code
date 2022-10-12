const emailRegex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{6,20}$/;

interface IValidation<T> {
  value: T;
  validators: ValidationFunc<T>[];
  error: IValidationError;
  changingValues?: T[];
}

interface IValidationError {
  message: string;
  setMessage: React.Dispatch<React.SetStateAction<string>>;
}
  
type ValidationFunc<T> = (value: T) => [boolean, string];

const validateLength = (value: string, minLength: number, maxLength: number, errorMessage: string): [boolean, string] => 
  validate(value.length >= minLength && value.length <= maxLength, errorMessage);

const validateRegex = (value: string, regex: RegExp, errorMessage: string): [boolean, string] => 
  validate(regex.test(value), errorMessage);

const validateMinLength = <Type extends { length: number }>(data: Type, minVal: number, errorMessage: string): [boolean, string] => 
  validate(data.length >= minVal, errorMessage);

const validateMaxLength = <Type extends { length: number }>(data: Type, maxValue: number, errorMessage: string): [boolean, string] => 
  validate(data.length <= maxValue, errorMessage);

const validateEquality = (first: string, second: string, errorMessage: string): [boolean, string] => 
  validate(first === second, errorMessage);

const validate = (condition: boolean, errorMessage: string): [boolean, string] => {
  if(condition) {
    return [true, ""];
  }

  return [false, errorMessage];
}

export { 
  validateLength, 
  validateRegex, 
  validateMinLength, 
  validateMaxLength, 
  validateEquality,
  emailRegex, 
  passwordRegex,
  ValidationFunc,
  IValidation,
  IValidationError
};
