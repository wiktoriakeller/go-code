import { View, Text, StyleSheet, GestureResponderEvent } from "react-native"
import React, { useState } from "react"
import { ICustomInputProps, CustomInput } from "../components/CustomInput";
import { IButtonProps, CustomButton } from "../components/CustomButton";
import colors from "../styles/colors";
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';

const SignInPage = ({ navigation  }) => {
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
        <CustomInput {...emailInput} />
        <CustomInput {...passwordInput} />
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
  textContainer: {
    marginLeft: 25,
    marginTop: '4%',
    marginBottom: '6%'
  },
  inputContainer: {
    flex: 1,
    alignItems: "center",
    justifyContent: "flex-start",
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