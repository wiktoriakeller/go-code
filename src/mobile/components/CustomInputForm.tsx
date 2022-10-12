import { View, TextInput, Text, StyleSheet, TextInputProps } from "react-native";
import React, { useState } from "react";
import Icon from "react-native-vector-icons/MaterialCommunityIcons";
import colors from "../styles/colors";
import { IValidation } from "../validation/validators";
import { useValidation } from "../hooks/useValidation";

interface IInputProps extends TextInputProps {
  label: string;
  validation: IValidation<string>;
  startIconName?: string;
  endIconName?: string;
  onPressEndIcon?: () => void;
}

const CustomInputForm = (props: IInputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [firstTimeFocused, setFirstTimeFocused] = useState(false);
  const startIconName = props.startIconName ?? "";
  const endIconName = props.endIconName ?? "";
  useValidation(props.validation);

  const getBorderColor = (): string => {
    if(props.validation.error.message.length > 0 && firstTimeFocused) {
      return colors.red;
    }
    
    if(isFocused) {
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

          { startIconName.length > 0
            ? <Icon 
                style={styles.startIcon} 
                name={startIconName}
              />
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

          { endIconName.length > 0
            ? <Icon 
                style={styles.endIcon} 
                name={endIconName}
                onPress={() => {
                  if(props.onPressEndIcon) {
                    props.onPressEndIcon();
                  }
                }}
              />
            : <View/>
          }
        </View>
          { props.validation.error.message.length > 0 && firstTimeFocused
            ? <Text style={styles.errorMessage}>{props.validation.error.message}</Text>
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
    height: 50,

    borderColor: colors.lightGrey,
    backgroundColor: colors.light
  },
  startIcon: {
    fontSize: 19,
    color: colors.primary,
    paddingLeft: 10
  },
  endIcon: {
    fontSize: 19,
    color: colors.primary,
    marginLeft: "-8%",
  },
  label: {
    marginBottom: 3,
    fontSize: 14,
    color: colors.black
  },
  input: {
    color: colors.black,
    fontSize: 16,
    paddingHorizontal: 6,
    width: "90%"
  },
  errorMessage: {
    color: colors.red,
    fontSize: 14,
    marginBottom: 2
  }
});

export { IInputProps, CustomInputForm };
