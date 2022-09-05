import { View, TextInput, StyleSheet, TextInputProps } from "react-native"
import colors from "../styles/colors"

interface ICustomInputProps extends TextInputProps {
  label: string;
}

const CustomInput = (props: ICustomInputProps) => {
  return (
    <View style={styles.container}>
      <View>{props.label}</View>
      <TextInput
        {...props}
        style={styles.input}
      />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.light,
    width: "88%",

    borderColor: colors.lightGrey,
    borderWidth: 1,
    borderRadius: 5,

    paddingHorizontal: 12,
    marginVertical: 6,
    height: 50,
    textAlign: "center"
  },
  label: {
    marginVertical: 5,
    fontSize: 14,
    color: colors.grey
  },
  input: {
    flex: 1,
    color: colors.grey,
    justifyContent: "flex-start",
    fontSize: 16
  }
});

export { ICustomInputProps, CustomInput };
