import { View, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useState } from "react"
import { ICustomInputProps, CustomInput } from "../components/CustomInput";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import colors from "../styles/colors";

const SignInScreen = () => {
  const [email, setEmail] = useState("");
  const emailInput = {
    text: email,
    placeholder: "Email",
    secureText: false,
    onChangeText: (value: string) => setEmail(value)
  };

  const [password, setPassword] = useState("");
  const passwordInput: ICustomInputProps = {
    value: password,
    placeholder: "Password",
    secureTextEntry: true,
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
      color: colors.grayText,
      fontWeight: 'normal'
    },
    onPress: (event: GestureResponderEvent) => {
      console.warn("Forgot password");
    }  
  };

  return (
    <View style={styles.root}>
      <Text style={styles.logoText}>Go Code</Text>
      <CustomInput {...emailInput} />
      <CustomInput {...passwordInput} />
      <View style={{ marginBottom: 20 }}/>
      <CustomButton {...loginButton} />
      <CustomButton {...signInGoogleButton} />
      <CustomButton {...forgotPasswordButton} />
    </View>
  )
}

const styles = StyleSheet.create({
  root: {
    flex: 1,
    alignItems: "center",
    justifyContent: "flex-start",
  },
  logoText: {
    marginTop: "25%",
    marginBottom: "15%",
    color: colors.primary,
    fontFamily: "sans-serif",
    fontSize: 48,
    fontWeight: "bold",
    fontStyle: "italic"
  }
});

export default SignInScreen;