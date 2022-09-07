import { View, Text, GestureResponderEvent } from "react-native"
import React, { useEffect, useState } from "react"
import { IInputProps, CustomInputForm, ValidationFunc } from "../components/CustomInputForm";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import { emailRegex, validateMinLength, validateRegex } from "./validators";
import { LoginNavigation } from "../navigation/navigationTypes";
import { mainFormStyle } from "./styles/commonStyles";
import colors from "../styles/colors";

const SignInPage = ({ navigation }: LoginNavigation) => {
  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const emailValidators = [
    (value: string) => validateMinLength(value, 1, "Email cannot be empty"),
    (value: string) => validateRegex(value, emailRegex, "Value must be an email")
  ];

  const emailInput: IInputProps = {
    value: email,
    secureTextEntry: false,
    placeholder: "Enter your email address",
    label: "Email",
    startIconName: "email-outline",
    validators: emailValidators,
    autoComplete: "email",
    error: {
      message: emailErrorMessage,
      setMessage: setEmailErrorMessage
    },
    onChangeText: (value: string) => setEmail(value)
  };

  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const [hidePassword, setHidePassword] = useState(true);
  const [endIconName, setEndIconName] = useState("eye-off-outline");
  const [secureTextEntry, setSecureTextEntry] = useState(true);
  const passwordValidators: ValidationFunc[] = [
    (value: string) => validateMinLength(value, 1, "Password cannot be empty")
  ];

  const passwordInput: IInputProps = {
    value: password,
    secureTextEntry: secureTextEntry,
    placeholder: "Enter your password",
    label: "Password",
    startIconName: "lock-outline",
    endIconName: endIconName,
    validators: passwordValidators,
    autoComplete: "password",
    error: {
      message: passwordErrorMessage,
      setMessage: setPasswordErrorMessage
    },
    onChangeText: (value: string) => setPassword(value),
    onPressEndIcon: () => {
      setHidePassword(!hidePassword);
      if(hidePassword) {
        setEndIconName("eye-off-outline");
        setSecureTextEntry(true);
      }
      else {
        setEndIconName("eye-outline");
        setSecureTextEntry(false);
      }
    }
  };

  const [disabledLoginButton, setDisabledLoginButton] = useState(false);
  const loginButton: IButtonProps = {
    text: "Sign in",
    isDisabled: disabledLoginButton,
    onPress: (event: GestureResponderEvent) => {
      console.warn("Sign in");
    }
  };

  useEffect(() => {
    if(emailErrorMessage.length > 0 || passwordErrorMessage.length > 0) {
      setDisabledLoginButton(true);
    }
    else {
      setDisabledLoginButton(false);
    }
  }, [emailErrorMessage, passwordErrorMessage]);

  const signInGoogleButton: IButtonProps = {
    text: "Sign in with Google",
    containerStyle: {
      color: colors.lightOrange,
      backgroundColor: colors.lightOrange
    },
    textStyle: {
      color: colors.orange,
    },
    isDisabled: false,
    onPress: (event: GestureResponderEvent) => {
      console.warn("Sign in Google");
    }  
  };

  const forgotPasswordButton: IButtonProps = {
    text: "Don't have an account? Register",
    containerStyle: {
      color: colors.tertiary,
      backgroundColor: colors.tertiary
    },
    textStyle: {
      color: colors.black,
      fontWeight: "normal"
    },
    isDisabled: false,
    onPress: (event: GestureResponderEvent) => {
      navigation.navigate("Register");
    }  
  };

  return (
    <View style={mainFormStyle.root}>
      <View style={mainFormStyle.textContainer}>
        <Text style={mainFormStyle.titleText}>Login</Text>
        <Text style={mainFormStyle.subText}>Enter your details to login</Text>
      </View>
      <View style={mainFormStyle.inputContainer}>
        <CustomInputForm {...emailInput} />
        <CustomInputForm {...passwordInput} />
        <View style={{ marginBottom: 10 }}/>
        <CustomButton {...loginButton} />
        <CustomButton {...signInGoogleButton} />
        <CustomButton {...forgotPasswordButton} />
      </View>
    </View>
  )
}

export default SignInPage;
