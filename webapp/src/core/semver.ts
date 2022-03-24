/**
 * Determines if a properly formatted semantic version contains a pre-release
 * label.
 * @param semver Version to check
 */
export function isPreRelease(semver?: string): boolean {
  return semver !== undefined && semver.indexOf("-") > -1;
}
