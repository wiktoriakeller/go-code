import { View, Text } from "react-native"
import React, { useEffect, useState } from "react"
import { IInputProps, CustomInputForm } from "../components/CustomInputForm";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import { validateMinLength, ValidationFunc } from "../validation/validators";
import { SignInNavigation } from "../navigation/stackNavigation";
import { mainFormStyle } from "./styles/formStyles";
import colors from "../styles/colors";
import { signIn, SignInResponse } from "../api/identity/signIn";
import { useHidePassword } from "../hooks/useHidePassword";
import { ApiResponse } from "../api/common";
import { Messages } from "../components/Messages";
import Spinner from "react-native-loading-spinner-overlay";

export const SignInPage = ({ navigation }: SignInNavigation) => {
  const [loading, setLoading] = useState(false);
  const [apiErrorMessages, setApiErrorMessages] = useState([] as string[]);

  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const emailValidators = [
    (value: string) => validateMinLength(value, 1, "Email cannot be empty"),
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
      },
    },    
    autoComplete: "email",
    onChangeText: (value: string) => {
      setEmail(value);
      setApiErrorMessages([]);
    }
  };

  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const hidePassowrd = useHidePassword();
  const passwordValidators: ValidationFunc<string>[] = [
    (value: string) => validateMinLength(value, 1, "Password cannot be empty")
  ];

  const passwordInput: IInputProps = {
    value: password,
    secureTextEntry: hidePassowrd.secureTextEntry,
    placeholder: "Enter your password",
    label: "Password",
    startIconName: "lock-outline",
    endIconName: hidePassowrd.endIconName,
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
      setPassword(value);
      setApiErrorMessages([]);
    },
    onPressEndIcon: () => hidePassowrd.hide()
  };

  const [disabledLoginButton, setDisabledLoginButton] = useState(false);
  const loginButton: IButtonProps = {
    text: "Sign in",
    isDisabled: disabledLoginButton,
    onPress: () => {
      setLoading(true);
      setApiErrorMessages([]);
      setDisabledLoginButton(true);

      signIn({
        email: email,
        password: password
      })
      .then(() => navigation.navigate("Home"))
      .catch((error: ApiResponse<SignInResponse>) => setApiErrorMessages(error.errors))
      .finally(() => {
        setLoading(false);
        setDisabledLoginButton(false);
      })
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
    onPress: () => console.log("Sign in google")
  };

  const signUpButton: IButtonProps = {
    text: "Don't have an account? Sign up!",
    containerStyle: {
      color: colors.tertiary,
      backgroundColor: colors.tertiary
    },
    textStyle: {
      color: colors.black,
      fontWeight: "normal"
    },
    isDisabled: false,
    onPress: () => navigation.navigate("SignUp")
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
        <Text style={mainFormStyle.titleText}>Sign In</Text>
        <Text style={mainFormStyle.subText}>Enter your details to login</Text>
      </View>
      <View style={mainFormStyle.inputContainer}>
        <CustomInputForm {...emailInput} />
        <CustomInputForm {...passwordInput} />
        <View style={{ marginBottom: 10 }}/>
        <CustomButton {...loginButton} />
        <CustomButton {...signInGoogleButton} />
        <CustomButton {...signUpButton} />
      </View>
    </View>
  )
}
