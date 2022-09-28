import { View } from "react-native"
import React, { useEffect, useState } from "react"
import { CourseListItem } from "../components/courses/CourseListItem"
import { ICourseInfo, getAllCoursesInfos, IGetAllCoursesInfosResponse } from "../api/courses/getAllCoursesInfos";
import { IApiResponse } from "../api/common";
import Spinner from "react-native-loading-spinner-overlay/lib";
import { ISignUpForCourseResponse, signUpForCourse } from "../api/courses/signUpForCourse";

interface IRegisterCourse {
  courseId: number;
  registered: boolean;
}

export const AllCoursesPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [courses, setCourses] = useState<ICourseInfo[]>([]);
  const [reload, setReload] = useState(true);

  useEffect(() => {
    getAllCoursesInfos()
    .then((response: IApiResponse<IGetAllCoursesInfosResponse>) => setCourses(response.data?.courses as ICourseInfo[]))
    .catch((error: IApiResponse<IGetAllCoursesInfosResponse>) => console.log(error))
    .finally(() => {
      setIsLoading(false);
    })
  }, [reload]);

  const registerForCourseButton = (props: IRegisterCourse) => {
    return {
      text: "Sign up",
      isDisabled: props.registered,
      onPress: () => {
        setIsLoading(true);
        signUpForCourse({
          id: props.courseId
        })
        .then(() => console.log("registered"))
        .catch((error: IApiResponse<ISignUpForCourseResponse>) => console.log(error))
        .finally(() => {
          setIsLoading(false);
          setReload(!reload);
        })
      }
    }
  };

  return (
    <View>
      <Spinner
        visible={isLoading}
        textContent={""}
      />
      {
        courses.map((item) => 
          <CourseListItem
            key={item.id}
            course={item}
            button={registerForCourseButton({ courseId: item.id, registered: item.isUserSignedUp })}
          />
        )
      }
    </View>
  )
}
