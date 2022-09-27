import { StyleSheet } from "react-native";
import colors from "../../styles/colors";

export const mainFormStyle = StyleSheet.create({
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
    marginTop: 10,
    color: colors.black,
    fontSize: 40,
    fontWeight: "bold"
  },
  subText: {
    color: colors.grey,
    fontSize: 18,
    marginTop: 4,
    marginBottom: 10
  }
});
