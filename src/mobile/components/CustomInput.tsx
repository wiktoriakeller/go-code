import { View, TextInput, StyleSheet, TextInputProps } from "react-native"
import colors from "../styles/colors"

interface ICustomInputProps extends TextInputProps {
}

const CustomInput = (props: ICustomInputProps) => {
  return (
    <View style={styles.container}>
      <TextInput
        {...props}
        style={styles.input}
      />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.white,
    width: "88%",

    borderColor: colors.lightGray,
    borderWidth: 1,
    borderRadius: 5,

    paddingHorizontal: 12,
    marginVertical: 8,
    height: 50,
    textAlign: "center"
  },
  input: {
    flex: 1,
    justifyContent: "flex-start",
    fontSize: 16
  }
});

export { ICustomInputProps, CustomInput };
