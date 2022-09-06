import { View, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useState } from "react"
import { ICustomInputProps, IValidationError, CustomInputForm, ValidationFunc } from "../components/CustomInputForm";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import colors from "../styles/colors";
import { validateLength } from "./validators";

const SignInPage = ({ navigation  }) => {
  const [email, setEmail] = useState("");
  const [emailErrorMessage, setEmailErrorMessage] = useState("");
  const emailValidators = [
    (value: string) => validateLength(value, 1, 20, "Email must be between 1 and 20 characters")
  ];

  const emailInput: ICustomInputProps = {
    value: email,
    secureTextEntry: false,
    placeholder: "Enter your email address",
    label: "Email",
    iconName: "email-outline",
    validators: emailValidators,
    error: {
      message: emailErrorMessage,
      setMessage: setEmailErrorMessage
    },
    onChangeText: (value: string) => setEmail(value)
  };

  const [password, setPassword] = useState("");
  const [passwordErrorMessage, setPasswordErrorMessage] = useState("");
  const passwordValidators: ValidationFunc[] = [];

  const passwordInput: ICustomInputProps = {
    value: password,
    secureTextEntry: true,
    placeholder: "Enter your password",
    label: "Password",
    validators: passwordValidators,
    error: {
      message: passwordErrorMessage,
      setMessage: setPasswordErrorMessage
    },
    onChangeText: (value: string) => setPassword(value)
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
      navigation.navigate("Forgot password");
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
    marginVertical: 10
  }
});

export default SignInPage;