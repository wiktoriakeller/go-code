import { View, Text } from "react-native"
import React, { useEffect, useState } from "react"
import { 
  emailRegex, 
  passwordRegex, 
  validateEquality, 
  validateLength, 
  validateRegex, 
  ValidationFunc 
} from "../validation/validators";
import { CustomInputForm, IInputProps } from "../components/CustomInputForm";
import { mainFormStyle } from "./styles/formStyles";
import { CustomButton, IButtonProps } from "../components/CustomButton";
import colors from "../styles/colors";
import { SignUpNavigation } from "../navigation/stackNavigation";
import { useHidePassword } from "../hooks/useHidePassword";
import { signUp, SignUpRequest } from "../api/identity/signUp";
import { ApiResponse } from "../api/common";
import Spinner from "react-native-loading-spinner-overlay/lib";
import { Messages } from "../components/Messages";

export const SignUpPage = ({ navigation }: SignUpNavigation) => {
  const [loading, setLoading] = useState(false);
  const [apiErrorMessages, setApiErrorMessages] = useState([] as string[]);

  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const emailValidators = [
    (value: string) => validateLength(value, 2, 20, "Email must be between 2 and 20 characters"),
    (value: string) => validateRegex(value, emailRegex, "Value must be an email")
  ];

  const emailInput: IInputProps = {
    value: email,
    secureTextEntry: false,
    placeholder: "Enter your email address",
    label: "Email",
    startIconName: "email-outline",
    validation: {
      value: email,
      validators: emailValidators,
      error: {
        message: emailErrorMessage,
        setMessage: setEmailErrorMessage
      }
    },
    onChangeText: (value: string) => { 
      setEmail(value)
      setApiErrorMessages([]);
    }
  };

  const [username, setUsername] = useState("");
  const [usernameErrorMessage, setUsernameErrorMessage] = useState("");
  const usernameValidators = [
    (value: string) => validateLength(value, 2, 20, "Username must be between 2 and 20 characters")
  ];

  const usernameInput: IInputProps = {
    value: username,
    secureTextEntry: false,
    placeholder: "Enter your username",
    label: "Username",
    startIconName: "pen",
    validation: {
      value: username,
      validators: usernameValidators,
      error: {
        message: usernameErrorMessage,
        setMessage: setUsernameErrorMessage
      },
    },    
    onChangeText: (value: string) => {
      setUsername(value)
      setApiErrorMessages([]);
    }
  };

  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const hidePassword = useHidePassword();
  const passwordValidators: ValidationFunc<string>[] = [
    (value: string) => validateLength(value, 6, 20, "Password must be between 6 and 20 characters"),
    (value: string) => validateRegex(value, passwordRegex, "Password must include uppercase and lowercase letters, a number and a special character")
  ];

  const passwordInput: IInputProps = {
    value: password,
    secureTextEntry: hidePassword.secureTextEntry,
    placeholder: "Enter your password",
    label: "Password",
    startIconName: "lock-outline",
    endIconName: hidePassword.endIconName,
    validation: {
      value: password,
      validators: passwordValidators,
      error: {
        message: passwordErrorMessage,
        setMessage: setPasswordErrorMessage
      },
    },     
    autoComplete: "password",
    onChangeText: (value: string) => { 
      setPassword(value)
      setApiErrorMessages([]);
    },
    onPressEndIcon: () => hidePassword.hide()
  };

  const [confirmPassword, setConfirmPassword] = useState("");
  const [confirmPasswordErrorMessage, setConfirmPasswordErrorMessage] = useState("");
  const hideConfirmPassword = useHidePassword();
  const confirmPasswordValidators: ValidationFunc<string>[] = [
    (value: string): [boolean, string] => validateEquality(value, password, "Password and confirm password should be equal")
  ];

  const confirmPasswordInput: IInputProps = {
    value: confirmPassword,
    secureTextEntry: hideConfirmPassword.secureTextEntry,
    placeholder: "Confirm your password",
    label: "Confirm password",
    startIconName: "lock-outline",
    endIconName: hideConfirmPassword.endIconName,
    validation: {
      value: confirmPassword,
      validators: confirmPasswordValidators,
      changingValues: [password, confirmPassword],
      error: {
        message: confirmPasswordErrorMessage,
        setMessage: setConfirmPasswordErrorMessage
      },
    },
    onChangeText: (value: string) => { 
      setConfirmPassword(value)
      setApiErrorMessages([]);
    },
    onPressEndIcon: () => hideConfirmPassword.hide()
  };

  const [disabledSignUpButton, setDisabledSignUpButton] = useState(false);
  const signUpButton: IButtonProps = {
    text: "Sign up",
    isDisabled: disabledSignUpButton,
    onPress: () => {
      setLoading(true);
      setApiErrorMessages([]);
      setDisabledSignUpButton(true);

      signUp({
        email: email,
        username: username,
        password: password
      })
      .then(() => navigation.goBack())
      .catch((error: ApiResponse<SignUpRequest>) => setApiErrorMessages(error.errors))
      .finally(() => {
        setLoading(false);
        setDisabledSignUpButton(false);
      })
    }
  };

  const errorMessages = [emailErrorMessage, usernameErrorMessage, passwordErrorMessage, confirmPasswordErrorMessage];
  useEffect(() => {
    for(const msg of errorMessages) {
      if(msg.length > 0) {
        setDisabledSignUpButton(true);
        return;
      }
    }

    setDisabledSignUpButton(false);
  }, errorMessages);

  const returnButton: IButtonProps = {
    text: "Already have an account? Sign in!",
    containerStyle: {
      color: colors.tertiary,
      backgroundColor: colors.tertiary
    },
    textStyle: {
      color: colors.black,
      fontWeight: "normal"
    },
    isDisabled: false,
    onPress: () => navigation.goBack()
  };

  return (
    <View style={mainFormStyle.root}>
      <Spinner
        visible={loading}
        textContent={""}
      />
      { apiErrorMessages.length > 0 
        ? <Messages messages={apiErrorMessages} isError={true}/>
        : <View/>
      }
      <View style={mainFormStyle.textContainer}>
        <Text style={mainFormStyle.titleText}>Sign Up</Text>
        <Text style={mainFormStyle.subText}>Create new account here</Text>
      </View>
      <View style={mainFormStyle.inputContainer}>
        <CustomInputForm {...emailInput} />
        <CustomInputForm {...usernameInput} />
        <CustomInputForm {...passwordInput} />
        <CustomInputForm {...confirmPasswordInput} />
        <View style={{ marginBottom: 10 }}/>
        <CustomButton {...signUpButton} />
        <CustomButton {...returnButton} />
      </View>
    </View>
  )
}
