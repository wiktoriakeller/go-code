import { View, Text, GestureResponderEvent } from 'react-native'
import React, { useEffect, useState } from 'react'
import { emailRegex, passwordRegex, validateLength, validateRegex } from './validators';
import { CustomInputForm, IInputProps, ValidationFunc } from '../components/CustomInputForm';
import { mainFormStyle } from './styles/commonStyles';
import { CustomButton, IButtonProps } from '../components/CustomButton';
import colors from '../styles/colors';
import { SignUpNavigation } from '../navigation/navigationTypes';

const SignUpPage = ({ navigation }: SignUpNavigation) => {
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

  const [username, setUsername] = useState("");
  const [usernameErrorMessage, setUsernameErrorMessage] = useState("");
  const usernameValidators = [
    (value: string) => validateLength(value, 2, 20, "Username must be between 2 and 20 characters"),
  ];

  const usernameInput: IInputProps = {
    value: username,
    secureTextEntry: false,
    placeholder: "Enter your username",
    label: "Username",
    startIconName: "pen",
    validators: usernameValidators,
    error: {
      message: usernameErrorMessage,
      setMessage: setUsernameErrorMessage
    },
    onChangeText: (value: string) => setUsername(value)
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

  const [confirmPassword, setConfirmPassword] = useState("");
  const [confirmPasswordErrorMessage, setConfirmPasswordErrorMessage] = useState("");
  const [hideConfirmPassword, setHideConfirmPassword] = useState(true);
  const [endIconNameConfirm, setEndIconNameConfirm] = useState("eye-off-outline");
  const [secureTextEntryConfirm, setSecureTextEntryConfirm] = useState(true);
  const confirmPasswordValidators: ValidationFunc[] = [
    (value: string): [boolean, string] => {
      if(value === password) {
        return [true, ""];
      }

      return [false, "Password and confirm password should be equal"];
    }
  ];

  const confirmPasswordInput: IInputProps = {
    value: confirmPassword,
    secureTextEntry: secureTextEntryConfirm,
    placeholder: "Confirm your password",
    label: "Confirm password",
    startIconName: "lock-outline",
    endIconName: endIconNameConfirm,
    validators: confirmPasswordValidators,
    error: {
      message: confirmPasswordErrorMessage,
      setMessage: setConfirmPasswordErrorMessage
    },
    onChangeText: (value: string) => setConfirmPassword(value),
    onPressEndIcon: () => {
      setHideConfirmPassword(!hideConfirmPassword);
      if(hideConfirmPassword) {
        setEndIconNameConfirm("eye-off-outline");
        setSecureTextEntryConfirm(true);
      }
      else {
        setEndIconNameConfirm("eye-outline");
        setSecureTextEntryConfirm(false);
      }
    }
  };

  const [disabledSignUpButton, setDisabledSignUpButton] = useState(false);
  const signUpButton: IButtonProps = {
    text: "Sign up",
    isDisabled: disabledSignUpButton,
    onPress: async (event: GestureResponderEvent) => {
      console.warn("Sign up");
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

  const goBackButton: IButtonProps = {
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
    onPress: async (event: GestureResponderEvent) => {
      navigation.goBack();
    }  
  };

  return (
    <View style={mainFormStyle.root}>
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
        <CustomButton {...goBackButton} />
      </View>
    </View>
  )
}

export default SignUpPage;
