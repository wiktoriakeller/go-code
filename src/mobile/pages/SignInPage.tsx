import { View, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useState } from "react"
import { IInputProps, CustomInputForm, ValidationFunc } from "../components/CustomInputForm";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import { emailRegex, passwordRegex, validateLength, validateRegex } from "./validators";
import { LoginNavigation } from "../navigation/navigationTypes";
import colors from "../styles/colors";

const SignInPage = ({ navigation }: LoginNavigation) => {
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
    validators: emailValidators,
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
    (value: string) => validateLength(value, 6, 20, "Password must be between 6 and 20 characters"),
    (value: string) => validateRegex(value, passwordRegex, "Password must include uppercase and lowercase letters, a number and a special character")
  ];

  const passwordInput: IInputProps = {
    value: password,
    secureTextEntry: secureTextEntry,
    placeholder: "Enter your password",
    label: "Password",
    startIconName: "lock-outline",
    endIconName: endIconName,
    validators: passwordValidators,
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

  const loginButton: IButtonProps = {
    text: "Sign in",
    onPress: (event: GestureResponderEvent) => {
      console.warn("Sign in");
    }
  };

  const signInGoogleButton: IButtonProps = {
    text: "Sign in with Google",
    containerStyle: {
      color: colors.lightOrange,
      backgroundColor: colors.lightOrange
    },
    textStyle: {
      color: colors.orange,
    },
    onPress: (event: GestureResponderEvent) => {
      console.warn("Sign in Google");
    }  
  };

  const forgotPasswordButton: IButtonProps = {
    text: "Forgot password?",
    containerStyle: {
      color: colors.tertiary,
      backgroundColor: colors.tertiary
    },
    textStyle: {
      color: colors.grey,
      fontWeight: "normal"
    },
    onPress: (event: GestureResponderEvent) => {
      navigation.navigate("ForgotPassword");
    }  
  };

  return (
    <View style={styles.root}>
      <View style={styles.textContainer}>
        <Text style={styles.titleText}>Login</Text>
        <Text style={styles.subText}>Enter your details to login</Text>
      </View>
      <View style={styles.inputContainer}>
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

const styles = StyleSheet.create({
  root: {
    flex: 1,
    backgroundColor: colors.background
  },
  inputContainer: {
    flex: 1,
    alignItems: "center",
    justifyContent: "flex-start"
  },
  textContainer: {
    marginLeft: "6%",
    marginBottom: 10
  },
  titleText: {
    color: colors.black,
    fontSize: 40,
    fontWeight: "bold"
  },
  subText: {
    color: colors.grey,
    fontSize: 18,
    marginTop: 4,
    marginBottom: 5
  }
});

export default SignInPage;