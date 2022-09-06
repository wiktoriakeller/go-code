import { View, TextInput, Text, StyleSheet, TextInputProps, NativeSyntheticEvent, TextInputFocusEventData } from "react-native";
import React, { useState, useEffect } from "react";
import Icon from "react-native-vector-icons/MaterialCommunityIcons";
import colors from "../styles/colors";

interface ICustomInputProps extends TextInputProps {
  label: string;
  validators: ValidationFunc[];
  error: IValidationError;
  iconName?: string;
}

interface IValidationError {
  message: string;
  setMessage: React.Dispatch<React.SetStateAction<string>>;
}

type ValidationFunc = (value: string) => [boolean, string];

const CustomInputForm = (props: ICustomInputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [firstTimeFocused, setFirstTimeFocused] = useState(false);

  useEffect(() => {
    if(props.validators !== null) {
      for(const validator of props.validators) {
        const result = validator(props.value);
        if(!result[0]) {
          props.error.setMessage(result[1]);
          return;
        }
      }
    }
    
    props.error.setMessage("");
  }, [props.value])

  const getBorderColor = (): string => {
    if(props.error.message.length > 0) {
      return colors.red;
    }
    else if(isFocused) {
      return colors.primary;
    }

    return colors.grey;
  };

  return (
    <View style={styles.container}>
      <Text style={styles.label}>{props.label}</Text>
      <View>
        <View style={[styles.inputContainer, 
          {
            borderColor: getBorderColor()
          }]}>

          { props.iconName !== null
            ? <Icon style={styles.icon} name={props.iconName}/>
            : <View/>
          }
          
          <TextInput
            {...props}
            
            onFocus={(e) => {
              if(props.onFocus) {
                props.onFocus(e);
              }
              
              if(!firstTimeFocused) {
                setFirstTimeFocused(true);
              }

              setIsFocused(true);
            }}

            onBlur={(e) => {
              if(props.onBlur) {
                props.onBlur(e);
              }
              setIsFocused(false);
            }}

            style={styles.input}
          />
        </View>
          { props.error.message.length > 0
            ? <Text style={styles.errorMessage}>{props.error.message}</Text>
            : <View/> 
          }
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    width: "88%",
    marginBottom: 8
  },
  inputContainer: {
    flex: 0,
    justifyContent: "flex-start",
    alignItems: "center",
    flexDirection: "row",
    textAlign: "center",
    width: "100%",

    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 10,
    height: 50,

    borderColor: colors.lightGrey,
    backgroundColor: colors.light
  },
  icon: {
    fontSize: 19,
    color: colors.primary
  },
  label: {
    marginBottom: 3,
    fontSize: 14,
    color: colors.black
  },
  input: {
    color: colors.black,
    fontSize: 16,
    paddingHorizontal: 6
  },
  errorMessage: {
    color: colors.red,
    fontSize: 14,
    marginBottom: 2
  }
});

export { ICustomInputProps, IValidationError, ValidationFunc, CustomInputForm };
