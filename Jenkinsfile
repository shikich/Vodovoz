node('Vod6') {
    stage('Build'){
        def REFERENCE_ABSOLUTE_PATH = "${JENKINS_HOME}/workspace/Vodovoz_Vodovoz_master"

        echo 'Prepare Vodovoz'	
        checkout changelog: false, poll: false, scm:([
            $class: 'GitSCM',
            branches: scm.branches,
            doGenerateSubmoduleConfigurations: scm.doGenerateSubmoduleConfigurations,
            extensions: scm.extensions 
                + [[$class: 'RelativeTargetDirectory', relativeTargetDir: 'Vodovoz']]
                + [[$class: 'CloneOption', reference: "${REFERENCE_ABSOLUTE_PATH}/Vodovoz"]],
            userRemoteConfigs: scm.userRemoteConfigs
        ])
        bat '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" Vodovoz\\Vodovoz.sln -t:Restore -p:Configuration=DebugWin -p:Platform=x86'

        echo 'Build solution'
        bat '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" Vodovoz\\Vodovoz.sln -t:Build -p:Configuration=DebugWin -p:Platform=x86'

        fileOperations([fileDeleteOperation(excludes: '', includes: 'VodovozWin.zip')])
        zip zipFile: 'VodovozWin.zip', archive: false, dir: 'Vodovoz/Vodovoz/bin/DebugWin'
        archiveArtifacts artifacts: 'VodovozWin.zip', onlyIfSuccessful: true
    }
    stage('Deploy'){
        echo env.JOB_NAME
        echo "Checking the deployment for a branch " + env.BRANCH_NAME
        script{
            def OUTPUT_PATH = "C:\\VodovozBuilds\\" + env.BRANCH_NAME
            if(
                env.BRANCH_NAME == 'master'
                || env.BRANCH_NAME == 'develop'
                || env.BRANCH_NAME == 'Beta'
                || env.BRANCH_NAME ==~ /^[Rr]elease(.*?)/)
            {
                echo "Deploy branch " + env.BRANCH_NAME
                copyArtifacts(projectName: '${JOB_NAME}', selector: specific( buildNumber: '${BUILD_NUMBER}'));
                unzip zipFile: 'VodovozWin.zip', dir: OUTPUT_PATH
            }else{
                echo "Nothing to deploy"
            }
        }
    }
}
